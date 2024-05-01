import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../../Services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  isLoading: boolean = false;
  errors : any = []

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
       email: ['',[ Validators.required, Validators.email, Validators.maxLength(50)]],
       password: ['', Validators.required], 
       confirmpassword: ['', Validators.required],                
    }, {
      validators: this.ConfirmedValidator('password', 'confirmpassword')        
    } as AbstractControlOptions
  );
 }

 constructor(private userService: UserService,
  private formBuilder: FormBuilder,
  private toastr: ToastrService,
  private router: Router) { }

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
  

  public RegisterUser() {
    this.isLoading = true;
    if (this.registerForm.valid) { 
      
      this.userService.registerUser(this.registerForm.value).subscribe({
              next: (res: any) => {
                //console.log(res);
                if(res.isSuccess)
                {
                  this.toastr.success(res.message, 'Information');
                  this.router.navigate(['/login']);
                }
                else
                {
                  this.toastr.warning(res.message, 'Information');
                }                  
              },
              error: (err: any) => {
                this.errors = err.error.errors;
              }
      });

      this.isLoading = false;
    }
    else {
      this.isLoading = false;
      this.registerForm.markAllAsTouched();
    }
    
  }
}
