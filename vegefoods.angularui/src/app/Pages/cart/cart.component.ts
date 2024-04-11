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
  totalAmount:number = 0;
  constructor(private productService: ProductService,
    private cartService: CartService,
    private toastr: ToastrService) { }

  ngOnInit(): void{
    this.getCartItems();
    this.totalAmount = this.calculateTotalAmount(this.cartproducts);
  } 

  getCartItems() {
    this.isLoading = true;
    this.cartproducts = this.cartService.getItems();
    console.log(this.cartproducts);
    this.isLoading = false;
  }
  calculateTotalAmount(items: CartProduct[]): number {
    return this.cartService.calculateTotalAmount(items);
  }
  removeProduct(item:any) :void{
    console.log(this.cartService)
    this.cartService.cartItems.splice(this.cartService.cartItems.findIndex(element=>item.id === element.id),1);
    //this.count= this.cartService.count;
  }
  changeQuantity(qty: any, price: number):void{
    console.log('change quantity: ', qty.target?.value, price);
    //this.count= this.cartService.count;     
  }
}
