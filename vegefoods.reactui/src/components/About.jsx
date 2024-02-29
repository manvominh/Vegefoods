import { Link } from "react-router-dom";

const About = () => {
    return (
        <>
            <section className="ftco-section ftco-no-pb ftco-no-pt bg-light fadeInUp ftco-animated">
                <div className="container">
                    <div className="row">
                        <div className="col-md-5 p-md-5 img img-2 d-flex justify-content-center align-items-center" style={{backgroundImage:'url(images/about.jpg)'}}>
                            <a href="https://vimeo.com/45830194" className="icon popup-vimeo d-flex justify-content-center align-items-center fadeInUp ftco-animated">
                                <span className="icon-play"></span>
                            </a>
                        </div>
                        <div className="col-md-7 py-5 wrap-about pb-md-5 ftco-animate fadeInUp ftco-animated">
                            <div className="heading-section-bold mb-4 mt-md-5 fadeInUp ftco-animated">
                                <div className="ml-md-0">
                                    <h2 className="mb-4">Welcome to Vegefoods an eCommerce website</h2>
                                </div>
                            </div>
                            <div className="pb-md-5">
                                <p>This is a demo web application that using ReactJS in FrontEnd</p>
                                <p>And ASP.Net Core Web API in BackEnd</p>
                                <p>Along with some technologies like: Entity Framework + CQRS pattern + Microsoft SQL Server</p>
                                <p>Author: Vo Minh Man</p>
                                <p>Contact: <a className="text" href="mailto:vominhman@gmail.com">vominhman@gmail.com</a></p>
                                <p><Link to="/shop" className="btn btn-primary">Shop now</Link></p>
                            </div>
                        </div>
                    </div>
                </div>
            </section>            
        </>
    )
};

export default About;