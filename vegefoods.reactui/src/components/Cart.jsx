import { Link } from "react-router-dom";
import { useContext } from 'react'
import { ShopContext } from "../context/ShopContext";
import { CartItem } from "./CartItem";

const Cart = () => {
    const { cartItems, addToCart, removeFromCart, clearCart, getTotalCartAmount } = useContext(ShopContext)
    const totalAmount = getTotalCartAmount();
    return (
		<section className="ftco-section ftco-cart">
			{totalAmount > 0 ? (
				<div className="container">
					<div className="row">
						<div className="col-md-12 ftco-animate fadeInUp ftco-animated">
							<div className="cart-list">
								<table className="table">
									<thead className="thead-primary">
										<tr className="text-center">
											<th>&nbsp;</th>
											<th>&nbsp;</th>
											<th>Product name</th>
											<th>Price</th>
											<th>Quantity</th>
											{/* <th>Total</th> */}
										</tr>
									</thead>
									<tbody>                                       
										{cartItems.map((product) => {
                                            
											if (cartItems[product.id] !== 0) {
												return <CartItem data={product} key={product.id} />;
											}
										})}
									</tbody>
								</table>
							</div>
						</div>
					</div>

					<div className="row justify-content-end">
						<div className="col-lg-4 mt-5 cart-wrap ftco-animate fadeInUp ftco-animated">
							<p></p>
						</div>
						<div className="col-lg-4 mt-5 cart-wrap ftco-animate fadeInUp ftco-animated">
							<p><Link to="/shop" className="btn btn-primary py-3 px-4">Continue Shopping</Link></p>
						</div>
						<div className="col-lg-4 mt-5 cart-wrap ftco-animate fadeInUp ftco-animated">
							<div className="cart-total mb-3">
								<h3>Cart Totals</h3>
								<p className="d-flex total-price">
									<span>Total</span>
									<span>${totalAmount}</span>
								</p>
							</div>
							<p><Link to="/" className="btn btn-primary py-3 px-4">Proceed To Checkout</Link></p>
						</div>
					</div>

				</div>
			) : (
				<div className="container">
					<div className="row">
						<div className="ml-md-0">
							<h2 className="mb-4">Your Shopping Cart is Empty</h2>
						</div>
					</div>
				</div>
			)}
		</section>
	);
}

export default Cart;