import { CartService } from '../../Services/cart.service';
import { ProductService } from './../../Services/product.service';
import { Product } from './product';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent implements OnInit {
  products!: Product[];
  isLoading: boolean = false;

  constructor(private productService: ProductService,
    private cartService: CartService,
    private toastr: ToastrService) { }

    ngOnInit(): void{
      this.getProducts();
    } 

    getProducts() {
      this.isLoading = true;
      this.productService.getProducts().subscribe((data) => {
        this.products = data;
        this.isLoading = false;
      });    
    }

    addToCart(product: Product) {
      this.cartService.addToCart(product);
      //window.alert('Your product has been added to the cart!');
    }

    getItemCount(id:number){
      return this.cartService.getItemCount(id);
    }
}
