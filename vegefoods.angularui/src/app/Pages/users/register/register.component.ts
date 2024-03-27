import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../../Services/user.service';

function validateEmail(control : any) {
  const value = control.value;
  if (value === null || value === '') {
    return null; // Allow empty input
  }
  const isValid = !isNaN(value) && Number.isInteger(Number(value));
  return isValid ? null : { invalidInteg: true };
}
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  isLoading: boolean = false;
  //loadingTitle: string = 'Loading ...';
  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
       email: ['', Validators.required],
       password: ['', Validators.required],                 
    });
 }

 constructor(private userService: UserService,
  private formBuilder: FormBuilder,
  private toastr: ToastrService,
  private router: Router) { }
  errors : any = []

   public RegisterUser() {
    console.log("register pressed");
    this.isLoading = true;
    //this.loadingTitle = "Registering a user ....";
    if (this.registerForm.valid) { 
      
      this.userService.registerUser(this.registerForm.value).subscribe({
              next: (res: any) => {
                this.toastr.success('Registered successfully', 'Information');
                this.router.navigate(['/login']);
              },
              error: (err: any) => {
                this.errors = err.error.errors;
              }
      });
      /* setTimeout(()=>{                           // <<<---using ()=> syntax
          console.log("setting time out");
          
      }, 60000); */
      this.isLoading = false;
    }
    else {
      this.isLoading = false;
      this.registerForm.markAllAsTouched();
    }
    
  }
}
