import React, { useContext } from "react";
import { ShopContext } from "../context/ShopContext";
import { MdDelete } from "react-icons/md";

export const CartItem = (props) => {
    const { id, name, price, imageURL } = props.data;
  //console.log(props.data);
  const { cartItems, removeFromCart, updateCartItemCount } = useContext(ShopContext);
  var imgUrl = {
    backgroundImage: 'url(' + imageURL + ')',    
  };
  return (
    <tr className="text-center">
      <td className="product-remove"><MdDelete style={{color: 'red', fontSize: '30px', cursor: 'pointer'}} onClick={(e) => removeFromCart(id)} /> </td> 

      <td className="image-prod"><div className="img" style={imgUrl} ></div></td>

      <td className="product-name">
        <h3>{name}</h3>
      </td>

      <td className="price">${price}</td>

      <td className="quantity">
        <div className="input-group mb-3">
          <input type="text" name="quantity" className="quantity form-control input-number" value={cartItems.find(item => item.id === id).quantity} onChange={(e) => updateCartItemCount(Number(e.target.value), id)}  min="1" max="100" />
        </div>
      </td>
      <td id={id} className="total">
          ${cartItems.find(item => item.id === id).quantity * price}
      </td>
    </tr>


  );
};
