import { AuthService } from './../../../Services/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  isLoading: boolean = false;

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
       email: ['', [ Validators.required, Validators.email, Validators.maxLength(50)]],
       password: ['', Validators.required],                 
    });
 }

 constructor(private authService: AuthService,
  private formBuilder: FormBuilder,
  private toastr: ToastrService,
  private router: Router) { }
  errors : any = []

   public Login() {
    this.isLoading = true;
    if (this.loginForm.valid) { 
      
      this.authService.login(this.loginForm.value).subscribe({
              next: (res: any) => {
                
                if(res.isSuccess)
                {
                  localStorage.setItem('email_vegefoods_angular', this.loginForm.value.email);
                  this.toastr.success('Logged In successfully', 'Information');
                  this.router.navigate(['/home']);
                }
                else {
                  this.toastr.warning('Invalid Credential', 'Warning');
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
      this.loginForm.markAllAsTouched();
    }
    
  }
}

