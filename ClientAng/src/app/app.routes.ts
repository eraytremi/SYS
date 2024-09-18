import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { authGuard } from './guard/auth.guard';
import { SupplierManagementComponent } from './pages/satin-alma/supplier-management/supplier-management.component';
import { RoleManagementComponent } from './pages/admin/role-management/role-management.component';
import { UserManagementComponent } from './pages/admin/user-management/user-management.component';
import { AdminComponent } from './pages/admin/admin.component';
import { SatinAlmaComponent } from './pages/satin-alma/satin-alma.component';

export const routes: Routes = [
    {
        path: '',
        redirectTo: 'login',
        pathMatch: 'full'
    },

    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'admin',
        component: AdminComponent,
        canActivate: [authGuard],
        children: [
            {
                path: 'role-management',
                component: RoleManagementComponent,
                canActivate: [authGuard]
            },
            {
                path: 'user-management',
                component: UserManagementComponent,
                canActivate: [authGuard]
            }

        ]
    },
    {
        path: 'satin-alma',
        component: SatinAlmaComponent,
        children: [
            {
                path: 'supplier-management',
                component: SupplierManagementComponent
            }
        ]
    }



];
