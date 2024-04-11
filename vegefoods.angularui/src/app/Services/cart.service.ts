import { Injectable } from '@angular/core';
import { Product } from '../Pages/products/product';
import { CartProduct } from '../Pages/cart/cartproduct';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  private cartUpdates = new Subject<string>();
  public cartUpdates$ = this.cartUpdates.asObservable();

  public cartItems:CartProduct[] =[];

  public get count():number{
    //return this.cartItems.reduce((c,t1) => t1.qty+c,0);
    return this.cartItems.length;//.reduce((c,t1) => t1.qty+c,0);
  };

  constructor() { }

  addToCart(product: Product) {
    
    let item:CartProduct = this.cartItems.find(item => item.id == product.id) as CartProduct ;

   if(item){ 
      item.qty ++;
    } else {
      let newCartProduct:CartProduct = { ...product, qty: 1 };
      this.cartItems.push(newCartProduct);
      console.log(this.cartItems);
    }

   this.cartUpdates.next('');    
  }

  

  getItems() {
    return this.cartItems;
  }

  clearCart() {
    //this.items = [];
    //return this.items;
  } 

  getItemCount(id:number){
    //this.items.find((i) => i.id === id)?.quantity || 0;
  }

  calculateTotalAmount(items: CartProduct[]): number {
    return items.reduce((total, item) => total + item.price * item.qty, 0);
  }
}
