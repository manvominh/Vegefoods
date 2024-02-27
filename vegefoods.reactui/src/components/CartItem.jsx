import React, { useContext, useState } from "react";
import { ShopContext } from "../context/ShopContext";

export const CartItem = (props) => {
    const { id, name, description, price, imageUrl, quantity } = props.data;
  console.log(props.data.quatity);
  const { cartItems, addToCart, removeFromCart, updateCartItemCount } = useContext(ShopContext);
  var imgUrl = {
    backgroundImage: 'url(' + imageUrl + ')',    
  };
  //console.log(cartItems);
  return (
    <tr className="text-center">
      <td class="product-remove"><a href="#"><span class="ion-ios-close" onClick={(e) => removeFromCart(id)}></span></a></td> 

      <td className="image-prod"><div className="img" style={imageUrl} ></div></td>

      <td className="product-name">
        <h3>{name}</h3>
        <p>{description}</p>
      </td>

      <td className="price">${price}</td>

      <td className="quantity">
        <div className="input-group mb-3">
          <input type="text" name="quantity" className="quantity form-control input-number" value={quantity} onChange={(e) => updateCartItemCount(Number(e.target.value), id)}	min="1" max="100" />
        </div>
      </td>

      {/* <td id={id} class="total">${totalPricePerProduct}</td> */}
    </tr>


  );
};
