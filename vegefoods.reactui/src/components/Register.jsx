import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import apihelper from '../helpers/apihelper';

const Register = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmpassword, setConfirmPassword] = useState("");

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
        if (confirmpassword === null || confirmpassword === '') {
            isproceed = false;
            errormessage += ' Confirm Password';
        }
        if (password !== confirmpassword) {
            isproceed = false;
            errormessage += ' Confirm Password: not match Password';
        }

        if(!isproceed){
            toast.warning(errormessage)
        }else{
            if(/^[a-zA-Z0-9]+@[a-zA-Z0-9]+\.[A-Za-z]+$/.test(email)){

            }else{
                isproceed = false;
                toast.warning('Please enter the valid email');
            }
            if(password.length < 8)    
            {
                isproceed = false;
                toast.warning('Password is not long enough. Password must be greater than 7 characters.')
            }        
        }
        return isproceed;
    }

    const handlesubmit = (e) => {
        e.preventDefault();
        let regobj = { email, password, confirmpassword };
        if (IsValidate()) {
            //console.log(regobj);
            apihelper.post("/users/register", regobj)
            .then((res) => {
                //console.log(res);
                if (res.data.isSuccess) {
                    toast.success('Registered successfully.')
                    navigate('/login');
                }
                else
                    toast.warn(res.data.message);
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
                                <input value={email} onChange={e => setEmail(e.target.value)} className="form-control"></input>
                            </div>                        
                            <div className="form-group text-left ">
                                <label>Password <span className="errmsg">*</span></label>
                                <input value={password} onChange={e => setPassword(e.target.value)} type="password" className="form-control"></input>
                            </div>
                            <div className="form-group text-left ">
                                <label>Confirm Password <span className="errmsg">*</span></label>
                                <input value={confirmpassword} onChange={e => setConfirmPassword(e.target.value)} type="password" className="form-control"></input>
                            </div>
                        </div>
                        <div className="card-footer">
                            <button type="submit" className="btn btn-primary">Register</button>&nbsp;&nbsp; 
                            <Link to={'/'} className="btn btn-danger">Close</Link>
                        </div>
                    </div>
                </form>
            </div>


        </div>
    );
}

export default Register;