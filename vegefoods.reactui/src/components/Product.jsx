import React,  { useContext } from "react";
import { ShopContext } from "../context/ShopContext";

export const Product = (props) => {


  const { id, name, description, price, imageUrl } = props.data;
  //const { addToCart, cartItems } = useContext(ShopContext);
  const { cartItems, addToCart , removeFromCart} = useContext(ShopContext)

  //const cartItem = cartItems.find((cartItem) => cartItem.id === id);
  //const cartItemCount = cartItem.quantity || 0;

  return (
    <div className="col-md-6 col-lg-3 ftco-animate fadeInUp ftco-animated">
                <div className="product" >
                    <a href="#" className="img-prod"><img className="img-fluid" src={imageUrl} alt={name} />
                        
                        <div className="overlay"></div>
                    </a>
                    <div className="text py-3 pb-4 px-3 text-center">
                        <h3><a href="#">{name}</a></h3>
                        <p>{description}</p>
                        <div className="d-flex">
                            <div className="pricing">
                                <p className="price"><span className="price-sale">${price}</span></p>
                                <p>
                                    <button className="btn btn-primary" onClick={() => addToCart(props.data)}>
                                    Add To Cart {/* {cartItemCount > 0 && <> ({cartItemCount})</>} */}
                                    </button>
                                </p>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
  );
};
