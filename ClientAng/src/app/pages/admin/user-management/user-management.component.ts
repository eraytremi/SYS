import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { ResultModel } from '../../../models/responsemodel';
import { UserModel } from '../../../models/usermodel';

@Component({
  selector: 'app-user-management',
  standalone: true,
  imports: [],
  templateUrl: './user-management.component.html',
  styleUrl: './user-management.component.css'
})
export class UserManagementComponent {
  userList: UserModel[] = [];

  ngOnInit(): void {
    this.getAllUser();
  }


  http = inject(HttpClient);
  getAllUser() {
    this.http.get<ResultModel<UserModel[]>>("https://localhost:7220/api/Users")
      .subscribe((res: ResultModel<UserModel[]>) => {
        if (res.data) {
          this.userList = res.data;
          console.log(this.userList);
        }
        console.log(res, 'res');
      });
  }

  openAddModal() {
    const openAddModal = document.getElementById("addUserModal");
    if (openAddModal != null) {
      openAddModal.style.display = "block";
    }
  }
}
