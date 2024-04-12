import { Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../../Services/user.service';
import { ToastrService } from 'ngx-toastr';
declare var $: any;

@Component({
  selector: 'app-changepassword',
  templateUrl: './changepassword.component.html',
  styleUrl: './changepassword.component.css'
})
export class ChangepasswordComponent {
  userPasswordForm!: FormGroup;
  changePasswordForm!: FormGroup;
  @Input() userId: any;

  isLoading: boolean = false;
  
  constructor(private userService: UserService,
    private toastr: ToastrService,
    private formBuilder: FormBuilder
    ) {
    //console.log("log: ", this.userId);

    this.userPasswordForm = this.formBuilder.group({
      id: ['', Validators.required],
      currentpassword: ['', Validators.required],
      newpassword: ['', Validators.required],       
      confirmpassword: ['', Validators.required],             
      }); 
       
    }

  CloseModel() {
    const modelDiv = document.getElementById('popupChangePassword');
    
    if(modelDiv!= null) {
      console.log('closed');
      modelDiv.style.display = 'none';
      $('#popupChangePassword').trigger('click');
    } 
  }
  changePassword(){
    this.isLoading = true;
    //if (this.userForm.valid) {             
      this.userPasswordForm.patchValue({
        id: this.userId
      });
      
      this.userService.changePassword(this.userPasswordForm.value).subscribe({
        next: (res: any) => {
          console.log(res);
          if(res){
            this.isLoading = false;           
            this.toastr.success('Changed password successfully', 'Information');
            this.CloseModel();            
          }         
          
        },
        error: (err: any) => {
          throw err;
        }
      });    
    //}  
  }
}
