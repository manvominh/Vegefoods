import React from 'react'
import { Modal, Button } from 'react-bootstrap'
import { useState } from "react";
import { toast } from "react-toastify";
import apihelper from '../helpers/apihelper';

const ChangePasswordDialog = (props) => {

    const [show, setShow] = useState(false);   
    const handleClose = () => setShow(!show);
    const handleShow = () => setShow(!show);

    const [currentpassword, setCurrentPassword] = useState("");
    const [newpassword, setNewPassword] = useState("");
    const [confirmpassword, setConfirmPassword] = useState("");

    const handlesubmit = (e) => {
        e.preventDefault();
        let changedPassword = { 
            Id: props.data, 
            CurrentPassword: currentpassword,
            NewPassword: newpassword,
            ConfirmPassword: confirmpassword
        }
        if (IsValidate()) {
            apihelper.post("/users/ChangePassword", changedPassword) 
            .then(response => {
                //console.log(response)
                if(response.data.isSuccess){
                    toast.success('Changed Password successfully.')
                    setShow(!show);
                }
                else
                  toast.warn(response.data.message);
            })
            .catch(error => {                
                toast.error('There was an error!', error);
            });             
        }
    }
  const IsValidate = () => {
        let isproceed = true;
        let errormessage = 'Please enter the value in ';       
        if (currentpassword === null || currentpassword === '') {
            isproceed = false;
            errormessage += ' Current Password';
        }
        if (newpassword === null || newpassword === '') {
            isproceed = false;
            errormessage += ' New Password';
        }
        if (currentpassword !== '' && newpassword !== '' && currentpassword === newpassword) {
            isproceed = false;
            errormessage += ' Current Password and New Password must be different.';
        }        
        //console.log(password, confirmpassword)
        if (confirmpassword === null || confirmpassword === '' || newpassword !== confirmpassword) {
            isproceed = false;
            errormessage += ' Confirm Password: not match Password';
        } 
        
        if(!isproceed){
            toast.warning(errormessage)
        }

        return isproceed;
    }  
  return (
    <>
      <Button variant="success" onClick={handleShow}>
            Change Password
      </Button>
      <Modal show={show}>
        <Modal.Header >
          <Modal.Title>Change Password</Modal.Title>        
          <button data-dismiss="modal" className="close" type="button" onClick={handleClose}>
            <span aria-hidden="true">×</span>
            <span className="sr-only">Close</span>
          </button>
        </Modal.Header>
        <Modal.Body>
            <input type="hidden" id="id" value={props.data} className="form-control"></input> 
            <div className="form-group row text-right">
                <label htmlFor="currentpassword" className="col-sm-4 col-form-label">Current Password <span className="errmsg">*</span></label>
                <div className="col-sm-8">
                <input id="currentpassword" value={currentpassword} onChange={e => setCurrentPassword(e.target.value)} type="password" className="form-control"  placeholder="Current Password" />
                </div>
            </div>
            <div className="form-group row text-right">
                <label htmlFor="newpassword" className="col-sm-4 col-form-label">Password <span className="errmsg">*</span></label>
                <div className="col-sm-8">
                <input id="newpassword" value={newpassword} onChange={e => setNewPassword(e.target.value)}  type="password" className="form-control"  placeholder="New Password" />
                </div>
            </div>
            <div className="form-group row text-right">
                <label htmlFor="confirmPassword" className="col-sm-4 col-form-label">Confirm Password <span className="errmsg">*</span></label>
                <div className="col-sm-8">
                <input id="confirmPassword" value={confirmpassword} onChange={e => setConfirmPassword(e.target.value)}  type="password" className="form-control"  placeholder="Confirm Password" />
                </div>
            </div> 
        </Modal.Body>
        <Modal.Footer>
          <Button variant="danger" onClick={handleClose}>
            Close
          </Button>
          <Button variant="primary" onClick={handlesubmit}>
            Save
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  )
}
export default ChangePasswordDialog