import { Product } from "../products/product";

export interface CartProduct extends Product {
    qty:number;
}