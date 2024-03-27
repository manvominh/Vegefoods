import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../../Services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  isLoading: boolean = false;
  //loadingTitle: string = 'Loading ...';
  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
       email: ['', Validators.required],
       password: ['', Validators.required],                 
    });
 }

 constructor(private userService: UserService,
  private formBuilder: FormBuilder,
  private toastr: ToastrService,
  private router: Router) { }
  errors : any = []

   public Login() {
    console.log("login pressed");
    this.isLoading = true;
    //this.loadingTitle = "Registering a user ....";
    if (this.loginForm.valid) { 
      
      this.userService.Login(this.loginForm.value).subscribe({
              next: (res: any) => {
                this.toastr.success('Logged In successfully', 'Information');
                this.router.navigate(['/home']);
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
      this.loginForm.markAllAsTouched();
    }
    
  }
}

