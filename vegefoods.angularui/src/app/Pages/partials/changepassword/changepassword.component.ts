import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-changepassword',
  templateUrl: './changepassword.component.html',
  styleUrl: './changepassword.component.css'
})
export class ChangepasswordComponent implements OnInit {
  userId;
  changePasswordForm!: FormGroup;

  
  constructor(@Inject(MAT_DIALOG_DATA) public data: {userId: number},
  private formBuilder: FormBuilder
    ) {
    this.userId = data.userId

    this.changePasswordForm = this.formBuilder.group({
      id: ['', Validators.required],
      currentpassword: ['', Validators.required],
      newpassword: ['', Validators.required],
      confirmpassword: ['', Validators.required],
      });
    }
  
  ngOnInit(): void {
    console.log('a');    
  }
}
