import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
//import Loading from "./Loading";

const Login = () => {
       
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const navigate = useNavigate();

    const ProceedLogin = (e) => {        
        e.preventDefault();
        
        if (!localStorage.getItem("email_vegefoods"))
            localStorage.setItem("email_vegefoods", "");

        let loginobj = { email, password };
        if (isValidate()) {
            fetch(process.env.REACT_APP_API+ "/users/login", {
                method: "POST",
                headers: { 'content-type': 'application/json' },
                body: JSON.stringify(loginobj)
            })
            .then((res) => {
                return res.json();
            }).then((resp) => {
                if (resp.isSuccess) {
                    localStorage.setItem('email_vegefoods', email);
                    localStorage.setItem('token_vegefoods', resp.token);
                    toast.success('Logged In successfully.')
                    navigate('/');
                 }
                 else{
                    toast.warn('Your credential is invalid. Please re-enter your credential.')
                 }
            })
            .catch((err) => {
                toast.error('Login Failed due to :' + err.message);
            });
        }
    }    
    const isValidate = () => {
        let result = true;
        if (email === '' || email === null) {
            result = false;
            toast.warning('Please enter Email');
        }
        if (password === '' || password === null) {
            result = false;
            toast.warning('Please enter Password');
        }
        if (password !== '' && password !== null) {
            if(/^[a-zA-Z0-9]+@[a-zA-Z0-9]+\.[A-Za-z]+$/.test(email)){

            }else{
                result = false;
                toast.warning('Please enter the valid email')
            }
        }
        return result;
    }
    return (
        <> 
            <div className="row">
                <div className="offset-lg-3 col-lg-6" style={{ marginTop: '100px' }}>
                    <form onSubmit={ProceedLogin} className="container">
                        <div className="card">
                            <div className="card-header text-left ">
                                <h2>Login</h2>
                            </div>
                            <div className="card-body">
                                <div className="form-group text-left">
                                    <label>Email <span className="errmsg">*</span></label>
                                    <input value={email} onChange={e => setEmail(e.target.value)} className="form-control"></input>
                                </div>
                                <div className="form-group text-left ">
                                    <label>Password <span className="errmsg">*</span></label>
                                    <input type="password" value={password} onChange={e => setPassword(e.target.value)} className="form-control"></input>
                                </div>
                            </div>
                            <div className="card-footer">
                                <button type="submit" className="btn btn-primary">Login</button> &nbsp;&nbsp;                                
                                <Link to={'/'} className="btn btn-danger">Close</Link>
                            </div>
                        </div>
                    </form>
                </div> 
            </div>        
        </>
    );
}

export default Login;