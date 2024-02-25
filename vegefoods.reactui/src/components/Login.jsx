import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import { RotatingLines  } from "react-loader-spinner";
import Loading from "./Loading";

const Login = () => {
    
    if (!localStorage.getItem("email_vegefoods"))
        localStorage.setItem("email_vegefoods", "");
        
    const [loading, setLoading] = useState(false);

    const [email, emailupdate] = useState('');
    const [password, passwordupdate] = useState('');

    const navigate = useNavigate();

    const ProceedLogin = (e) => {
        e.preventDefault();
        let loginobj = { email, password };
        if (validate()) {
            fetch(process.env.REACT_APP_API+ "/users/login", {
                method: "POST",
                headers: { 'content-type': 'application/json' },
                body: JSON.stringify(loginobj)
            })
            .then((res) => {
                return res.json();
            }).then((resp) => {
                console.log(resp.isSuccess)
                if (resp.isSuccess) {
                    localStorage.setItem('email_vegefoods', email);
                    toast.success('Logged In successfully.')
                    navigate('/');
                 }
                 else{
                    toast.warn('Please re-enter your credential.')
                 }
            })
            .catch((err) => {
                toast.error('Login Failed due to :' + err.message);
            });
        }
    }    
    const validate = () => {
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
        <> {loading && <Loading />}
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
                                    <input value={email} onChange={e => emailupdate(e.target.value)} className="form-control"></input>
                                </div>
                                <div className="form-group text-left ">
                                    <label>Password <span className="errmsg">*</span></label>
                                    <input type="password" value={password} onChange={e => passwordupdate(e.target.value)} className="form-control"></input>
                                </div>
                            </div>
                            <div className="card-footer">
                                <button type="submit" className="btn btn-primary">Login</button> &nbsp;&nbsp;
                                <Link className="btn btn-success" to={'/register'}>New User</Link>
                            
                            </div>
                        </div>
                    </form>
                </div> 
            </div>        
        <button onClick={() => setLoading(!loading) } className="btn btn-primary">Start Loading</button> 
        </>
    );
}

export default Login;