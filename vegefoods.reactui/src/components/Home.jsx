import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";

const Home = () => {


    return (
        <>
            <section className="ftco-section">
                <div className="container">
                    <div className="row no-gutters ftco-services">
                        <div className="col-md-3 text-center d-flex align-self-stretch ftco-animate fadeInUp ftco-animated">
                            <div className="media block-6 services mb-md-0 mb-4">
                                <div className="icon bg-color-1 active d-flex justify-content-center align-items-center mb-2">
                                    <span className="flaticon-shipped"></span>
                                </div>
                                <div className="media-body">
                                    <h3 className="heading">Free Shipping</h3>
                                    <span>On order over $100</span>
                                </div>
                            </div>
                        </div>
                        <div className="col-md-3 text-center d-flex align-self-stretch ftco-animate fadeInUp ftco-animated">
                            <div className="media block-6 services mb-md-0 mb-4">
                                <div className="icon bg-color-2 d-flex justify-content-center align-items-center mb-2">
                                    <span className="flaticon-diet"></span>
                                </div>
                                <div className="media-body">
                                    <h3 className="heading">Always Fresh</h3>
                                    <span>Product well package</span>
                                </div>
                            </div>
                        </div>
                        <div className="col-md-3 text-center d-flex align-self-stretch ftco-animate fadeInUp ftco-animated">
                            <div className="media block-6 services mb-md-0 mb-4">
                                <div className="icon bg-color-3 d-flex justify-content-center align-items-center mb-2">
                                    <span className="flaticon-award"></span>
                                </div>
                                <div className="media-body">
                                    <h3 className="heading">Superior Quality</h3>
                                    <span>Quality Products</span>
                                </div>
                            </div>
                        </div>
                        <div className="col-md-3 text-center d-flex align-self-stretch ftco-animate fadeInUp ftco-animated">
                            <div className="media block-6 services mb-md-0 mb-4">
                                <div className="icon bg-color-4 d-flex justify-content-center align-items-center mb-2">
                                    <span className="flaticon-customer-service"></span>
                                </div>
                                <div className="media-body">
                                    <h3 className="heading">Support</h3>
                                    <span>24/7 Support</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <section className="ftco-section ftco-category ftco-no-pt">
                <div className="container">
                    <div className="row">
                        <div className="col-md-8">
                            <div className="row">
                                <div className="col-md-6 order-md-last align-items-stretch d-flex">
                                    <div className="category-wrap-2 ftco-animate img align-self-stretch d-flex fadeInUp ftco-animated" style={{ backgroundImage: 'url(images/category.jpg)' }} >
                                        <div className="text text-center">
                                            <h2>Vegetables</h2>
                                            <p>Protect the health of every home</p>
                                            <p><Link to="/shop" className="btn btn-primary">Shop now</Link></p>
                                        </div>
                                    </div>
                                </div>
                                <div className="col-md-6">
                                    <div className="category-wrap ftco-animate img mb-4 d-flex align-items-end fadeInUp ftco-animated" style={{ backgroundImage: 'url(images/category-1.jpg)' }} >
                                        <div className="text px-3 py-1">
                                            <h2 className="mb-0"><a href="/shop">Fruits</a></h2>
                                        </div>
                                    </div>
                                    <div className="category-wrap ftco-animate img d-flex align-items-end fadeInUp ftco-animated" style={{ backgroundImage: 'url(images/category-2.jpg)' }}>
                                        <div className="text px-3 py-1">
                                            <h2 className="mb-0"><a href="/shop">Vegetables</a></h2>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div className="col-md-4">
                            <div className="category-wrap ftco-animate img mb-4 d-flex align-items-end fadeInUp ftco-animated" style={{ backgroundImage: 'url(images/category-3.jpg)' }}>
                                <div className="text px-3 py-1">
                                    <h2 className="mb-0"><a href="/shop">Juices</a></h2>
                                </div>
                            </div>
                            <div className="category-wrap ftco-animate img d-flex align-items-end fadeInUp ftco-animated" style={{ backgroundImage: 'url(images/category-4.jpg)' }}>
                                <div className="text px-3 py-1">
                                    <h2 className="mb-0"><a href="/shop">Dried</a></h2>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </>
    );
}

export default Home;