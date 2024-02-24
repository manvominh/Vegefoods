import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

const Register = () => {
    const [email, emailchange] = useState("");
    const [password, passwordchange] = useState("");

    const navigate = useNavigate();

    const IsValidate = () => {
        let isproceed = true;
        let errormessage = 'Please enter the value in ';
        
        if (email === null || email === '') {
            isproceed = false;
            errormessage += ' Email';
        }
        if (password === null || password === '') {
            isproceed = false;
            errormessage += ' Password';
        }
        

        if(!isproceed){
            toast.warning(errormessage)
        }else{
            if(/^[a-zA-Z0-9]+@[a-zA-Z0-9]+\.[A-Za-z]+$/.test(email)){

            }else{
                isproceed = false;
                toast.warning('Please enter the valid email')
            }
        }
        return isproceed;
    }

    const handlesubmit = (e) => {
        e.preventDefault();
        let regobj = { email, password };
        if (IsValidate()) {
        //console.log(regobj);
        fetch(process.env.REACT_APP_API+ "/users/register", {
                method: "POST",
                headers: { 'content-type': 'application/json' },
                body: JSON.stringify(regobj)
            }).then((res) => {
                if (res.status == 200) {
                    toast.success('Registered successfully.')
                    navigate('/login');
                }
            }).catch((err) => {
                toast.error('Failed :' + err.message);
            });
        }
    }
    return (
        <div className="ftco-section">
            <div className="offset-lg-3 col-lg-6">
                <form className="container" onSubmit={handlesubmit}>
                    <div className="card">
                        <div className="card-header text-left">
                            <h1>Register</h1>
                        </div>
                        <div className="card-body">
                            <div className="form-group text-left ">
                                <label>Email <span className="errmsg">*</span></label>
                                <input value={email} onChange={e => emailchange(e.target.value)} className="form-control"></input>
                            </div>                        
                            <div className="form-group text-left ">
                                <label>Password <span className="errmsg">*</span></label>
                                <input value={password} onChange={e => passwordchange(e.target.value)} type="password" className="form-control"></input>
                            </div>
                        </div>
                        <div className="card-footer">
                            <button type="submit" className="btn btn-primary">Register</button>&nbsp;&nbsp; 
                            <Link to={'/login'} className="btn btn-danger">Close</Link>
                        </div>
                    </div>
                </form>
            </div>


        </div>
    );
}

export default Register;