import React from "react";

const Footer = () => {
    return <footer className="ftco-footer ftco-section">
    <div className="container">        
      <div className="row">
      <div className="col-md-12 text-center">
        <p>
          Author: Man Vo | Contact to <a href="mailto:vominhman@gmail.com">Man Vo</a> | Vegefoods by ReactJS
          </p>
        </div>
        <div className="col-md-12 text-center">
        <p>
          {/* <!-- Link back to Colorlib can't be removed. Template is licensed under CC BY 3.0. --> */}
                        Copyright &copy;<script>document.write(new Date().getFullYear());</script> All rights reserved | This template is made with <i className="icon-heart color-danger" aria-hidden="true"></i> by <a href="https://colorlib.com" target="_blank">Colorlib</a>
                        {/* <!-- Link back to Colorlib can't be removed. Template is licensed under CC BY 3.0. --> */}
          </p>
        </div>
      </div>
    </div>
  </footer>;
};

export default Footer;