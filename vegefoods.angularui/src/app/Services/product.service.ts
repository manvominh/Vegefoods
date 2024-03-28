import { Product } from './../Pages/products/product';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private baseUrl:string = environment.baseUrl;
  constructor(private httpClient: HttpClient) { }

  getProducts(){
    return this.httpClient.get<Product[]>(`${this.baseUrl}/Products`);
  }
}
