import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Product } from './../Pages/products/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private baseUrl:string = environment.baseUrl;
  constructor(private httpClient: HttpClient) { }

  getProducts(): Observable<Product[]> {
    return this.httpClient.get<Product[]>(`${this.baseUrl}/Products`);
  }
}
