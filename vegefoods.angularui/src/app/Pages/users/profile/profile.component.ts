import { Component, OnInit } from '@angular/core';
import { ActivatedRoute,Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../../Services/user.service';
// need to remove component also
//import { ChangepasswordComponent } from '../../partials/changepassword/changepassword.component';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit{
  userId !: any;
  email !: any;
  user!: any;
  userForm!: FormGroup;
  userPasswordForm!: FormGroup;
  isLoading: boolean = false;
  errors: any;

  constructor(private activatedRoute: ActivatedRoute,
    private userService: UserService,
    private formBuilder: FormBuilder,
   private toastr: ToastrService,
  private router: Router) {

  this.userForm = this.formBuilder.group({
    id: ['', Validators.required],
    email: ['', Validators.required],
    password: ['', Validators.required],
    firstName: ['', Validators.maxLength(50)],
    lastName: ['', Validators.maxLength(50)],
    //phone: ['', [Validators.required, validInteger]],
    phone: ['', Validators.maxLength(10)],
    country: ['', Validators.maxLength(50)],
    address: ['', Validators.maxLength(200)],
    gender: ['', Validators.maxLength(10)],              
    dateOfBirth: ['', Validators.maxLength(10)],              
    });

    this.userPasswordForm = this.formBuilder.group({
      id: ['', Validators.required],
      currentpassword: ['', Validators.required],
      newpassword: ['', Validators.required],       
      confirmpassword: ['', Validators.required],             
      }); 
  }
  ngOnInit(): void {
    this.email = localStorage.getItem('email_vegefoods_angular');

    if(this.email === '')
      this.router.navigate(['/login']);

    this.userService.getUserByEmail(this.email).subscribe(
      (res: any) => {
      this.user = res;
      this.userId = res.id;
      this.userForm.patchValue(this.user);
      this.userPasswordForm.patchValue(this.user);
      }
    );
  }

  updateUser() {
    
    this.isLoading = true;
    //if (this.userForm.valid) { 
      console.log(this.userId);
      console.log('updating')
       this.userService.updateUser(this.userForm.value, this.userId).subscribe({
        next: (res: any) => {
          this.isLoading = false;
          this.toastr.success('Updated profile successfully', 'Information');
        },
        error: (err: any) => {
          throw err;
        }
      });      
      
    // }    
  }

  cancelUpdate(){
    this.router.navigate(['/home']);
  }
}
