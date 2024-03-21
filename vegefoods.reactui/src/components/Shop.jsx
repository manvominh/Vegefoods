import React from "react";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import {Product}  from "./Product";
import api from '../helpers/api';

const Shop = () => {
    const [products, setProducts] = useState([]) ;

    useEffect(() => {
        
        api.get("/products")        
        .then(response => {
          setProducts(response.data);
        })
        .catch((err) => {
            toast.error('Fetch Products Failed due to :' + err.message);
        });
        
      }, []);
  return (
    <div className="container">
      <div className="shopTitle">
        <h1>Man Vo Shopping Cart</h1>
      </div>

      <div className="row">
        {products.map((product) => (
          <Product data={product} key={product.id} />
        ))}
        
      </div>
    </div>
  );
};

export default Shop;