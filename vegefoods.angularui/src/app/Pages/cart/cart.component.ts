import { Component, OnInit } from '@angular/core';
import { CartService } from '../../Services/cart.service';
import { ProductService } from './../../Services/product.service';
import { ToastrService } from 'ngx-toastr';
import { CartProduct } from './cartproduct';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent implements OnInit {
  cartproducts!: CartProduct[];
  isLoading: boolean = false;
  //totalAmount:number = 0;
  constructor(private productService: ProductService,
    private cartService: CartService,
    private toastr: ToastrService) { }

  ngOnInit(): void{
    this.getCartItems();
    //this.totalAmount = this.calculateTotalAmount();
  } 

  getCartItems() {
    this.isLoading = true;
    this.cartproducts = this.cartService.getItems();
    console.log(this.cartproducts);
    this.isLoading = false;
  }
  calculateTotalAmount(): number {
    return this.cartService.calculateTotalAmount();
  }
  removeProduct(item:any) :void{
    this.cartService.cartItems.splice(this.cartService.cartItems.findIndex(element=>item.id === element.id),1);
    this.calculateTotalAmount();
    //this.cartService.getNumbersOfCart();
  }
  changeQuantity(qty: any, cartproduct: CartProduct):void{
    //console.log('change quantity: ', qty.target?.value, cartproduct);
    let changedqty = qty.target?.value;
    if(changedqty !== 0 && changedqty !== '' && !isNaN(changedqty) ){
      //console.log('change qty')
      this.cartService.cartItems.map((cartItem) =>
          cartItem.id === cartproduct.id
            ? cartItem.qty = changedqty 
            : cartItem
        );        
    }
    else {
      this.cartService.cartItems.map((cartItem) =>
        cartItem.id === cartproduct.id
          ? cartItem.qty = 0 
          : cartItem
      );
    }
    this.calculateTotalAmount(); 
  }  
  processCheckout():void{    
    this.toastr.success('Checkout process is in progress. Thank you for your order.', 'Information');
  }
}
