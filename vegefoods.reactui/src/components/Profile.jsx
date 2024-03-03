import { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

const Profile = () => {
    const [id, setId] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmpassword, setConfirmPassword] = useState("");
    const [firstname, setFirstName] = useState("");
    const [lastname, setLastName] = useState("");
    const [phone, setPhone] = useState("");
    const [country, setCountry] = useState("vietnam");
    const [address, setAddress] = useState("");
    const [gender, setGender] = useState("female");
    const [dateofbirth, setDateOfBirth] = useState(new Date());

    const navigate = useNavigate();
    let email_vegefoods = localStorage.getItem('email_vegefoods');
    useEffect(() => {
        
        if(email_vegefoods === '' || email_vegefoods === null){
         navigate('/login');
        }
        else{
         //displayusernameupdate(username);
         fetch(process.env.REACT_APP_API+"/users/GetUserByEmail/" + email_vegefoods).then((res) => {
                return res.json();
            }).then((resp) => {
                //console.log(resp)
                setId(resp.id);
                setEmail(resp.email);
                setPassword(resp.password);
                setConfirmPassword(resp.password);
                setFirstName(resp.firstName);
                setLastName(resp.lastName);
                setPhone(resp.phone);
                setCountry(resp.country);
                setAddress(resp.address);
                setGender(resp.gender);
                setDateOfBirth(resp.dateOfBirth);
            }).catch((err) => {
                toast.error('Get Profile Information Failed due to :' + err.message);
            });
        }
 
     }, []);

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
        //console.log(password, confirmpassword)
        if (confirmpassword === null || confirmpassword === '' || password !== confirmpassword) {
            isproceed = false;
            errormessage += ' Confirm Password: not match Password';
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
        //let profileUser = { id, email, password, firstname, lastname, phone, country, address, gender, dateofbirth };
        if (IsValidate()) {
        //console.log(profileUser);
        fetch(process.env.REACT_APP_API+"/users/" + id, {
                method: "PUT",
                headers: { 'content-type': 'application/json' },
                body: JSON.stringify({ 
                    Id: id, 
                    Email: email,
                    Password: password,
                    ConfirmPassword: confirmpassword,
                    FirstName: firstname || "",
                    DateOfBirth: dateofbirth,
                    LastName: lastname || "",
                    Phone: phone || "",
                    Country: country || "",
                    Gender: gender || "",
                    Address: address || ""
                })                
            }).then((res) => {
                if(res.ok)
                    toast.success('Update Profile Information successfully.')                
            }).catch((err) => {
                toast.error('Failed :' + err.message);
            });
        }
    }

    return (
        <div className="ftco-section">
            <div className="offset-lg-3 col-lg-6">
                <h1>Profile Information</h1>
                <form onSubmit={handlesubmit}>
                    <input type="hidden" value={id} className="form-control"></input> 
                    <div className="form-group row text-right">
                        <label htmlFor="email" className="col-sm-4 col-form-label">Email</label>
                        <div className="col-sm-8">
                        <input type="text" readOnly id="email" className="form-control-plaintext"  value={email} />
                        </div>
                    </div>
                    <div className="form-group row text-right">
                        <label htmlFor="password" className="col-sm-4 col-form-label">Password <span className="errmsg">*</span></label>
                        <div className="col-sm-8">
                        <input id="password" value={password} onChange={e => setPassword(e.target.value)} type="password" className="form-control"  placeholder="Password" />
                        </div>
                    </div>
                    <div className="form-group row text-right">
                        <label htmlFor="confirmPassword" className="col-sm-4 col-form-label">Confirm Password <span className="errmsg">*</span></label>
                        <div className="col-sm-8">
                        <input id="confirmPassword" value={confirmpassword} onChange={e => setConfirmPassword(e.target.value)} type="password" className="form-control"  placeholder="Confirm Password" />
                        </div>
                    </div>
                    <div className="form-group row text-right">
                        <label htmlFor="firstname" className="col-sm-4 col-form-label">First Name</label>
                        <div className="col-sm-8">
                        <input id="firstname" value={firstname || ""} onChange={e => setFirstName(e.target.value)} className="form-control"></input>
                        </div>
                    </div>
                    <div className="form-group row text-right">
                        <label htmlFor="lastname" className="col-sm-4 col-form-label">Last Name</label>
                        <div className="col-sm-8">
                        <input id="lastname" value={lastname || ""} onChange={e => setLastName(e.target.value)} className="form-control"></input>
                        </div>
                    </div>
                    <div className="form-group row text-right">
                        <label htmlFor="dateOfBirth" className="col-sm-4 col-form-label">Date Of Birth</label>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <DatePicker id="dateOfBirth" showIcon selected={dateofbirth || ""} onChange={(date) => setDateOfBirth(date)} />
                        
                    </div>
                    <div className="form-group row text-right">
                        <label htmlFor="phone" className="col-sm-4 col-form-label">Phone</label>
                        <div className="col-sm-8">
                        <input id="phone" value={phone || ""} onChange={e => setPhone(e.target.value)} className="form-control"></input>
                        </div>
                    </div>

                    <div className="form-group row text-right">
                        <label htmlFor="country" className="col-sm-4 col-form-label">Country</label>
                        <div className="col-sm-8">
                            <select id="country" value={country || "vietnam"} onChange={e => setCountry(e.target.value)} className="form-control">
                                <option value="india">India</option>
                                <option value="usa">USA</option>
                                <option value="singapore">Singapore</option>
                                <option value="vietnam">VietNam</option>
                            </select>
                        </div>
                    </div>
                    <div className="form-group row text-right">
                        {console.log(gender)}
                        <label htmlFor="gender" className="col-sm-4 col-form-label">Gender</label>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <input id="gender" type="radio" checked={gender === 'male'} onChange={e => setGender(e.target.value)} name="gender" value="male" className="app-check"></input>
                        &nbsp;&nbsp; 
                        <label>Male</label>&nbsp;&nbsp; &nbsp;&nbsp; 
                        <input type="radio" checked={gender === 'female'} onChange={e => setGender(e.target.value)} name="gender" value="female" className="app-check"></input>
                        &nbsp;&nbsp; 
                        <label>Female</label>
                    </div>
                    <div className="form-group row text-right">
                        <label htmlFor="address" className="col-sm-4 col-form-label">Address</label>
                        <div className="col-sm-8">
                        <textarea id="address" value={address || ""} onChange={e => setAddress(e.target.value)} className="form-control"></textarea>
                        </div>
                    </div>
                    <div className="text-center">
                        <button type="submit" className="btn btn-primary">Update</button> &nbsp;&nbsp; 
                        <Link to={'/'} className="btn btn-danger">Close</Link>
                    </div>
                </form>                
            </div>
        </div>
    );
}

export default Profile;