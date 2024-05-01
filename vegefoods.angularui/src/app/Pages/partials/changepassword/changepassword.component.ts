import { Component, Input } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../../Services/user.service';
import { ToastrService } from 'ngx-toastr';
declare var $: any;

@Component({
  selector: 'app-changepassword',
  templateUrl: './changepassword.component.html',
  styleUrl: './changepassword.component.css'
})
export class ChangepasswordComponent {
  userChangePasswordForm!: FormGroup;
  @Input() userId: any;

  isLoading: boolean = false;
  
  constructor(private userService: UserService,
    private toastr: ToastrService,
    private formBuilder: FormBuilder
    ) {
    //console.log("log: ", this.userId);

    this.userChangePasswordForm = this.formBuilder.group({
      id: ['', Validators.required],
      currentpassword: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(50)]],
      newpassword: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(50)]],       
      confirmpassword: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(50)]],             
      }, {
        validators: [this.ConfirmedValidator('newpassword', 'confirmpassword'),
          this.ChangedPasswordValidator('currentpassword', 'newpassword')]
      } as AbstractControlOptions
    ); 
      
  }
  ChangedPasswordValidator(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];
      if (
        matchingControl.errors &&
        !matchingControl.errors?.['changedPasswordValidator']
      ) {
        return;
      }
      if (control.value === matchingControl.value) {
        matchingControl.setErrors({ changedPasswordValidator: true });
      } else {
        matchingControl.setErrors(null);
      }
    };
  }

  ConfirmedValidator(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];
      if (
        matchingControl.errors &&
        !matchingControl.errors?.['confirmedValidator']
      ) {
        return;
      }
      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ confirmedValidator: true });
      } else {
        matchingControl.setErrors(null);
      }
    };
  }
  CloseModel() {
    
    const modelDiv = document.getElementById('popupChangePassword');    
    if(modelDiv!= null) {
      //console.log('closed');
      modelDiv.style.display = 'none';
      $('#popupChangePassword').trigger('click');
    } 
    this.userChangePasswordForm.reset();
  }
  changePassword(){
    this.isLoading = true;
    if (this.userChangePasswordForm.valid) {               
      this.userService.changePassword(this.userChangePasswordForm.value).subscribe({
        next: (res: any) => {
          //console.log(res);
          this.isLoading = false;   
          if(res.isSuccess){                    
            this.toastr.success(res.message, 'Information');
            this.CloseModel();            
          }         
          else
            this.toastr.warning(res.message, 'Information');
        },
        error: (err: any) => {
          throw err;
        }
      });    
    }  
  }
}
