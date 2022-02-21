import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserModule } from './user/user.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { EmployeeModule } from './employee/employee.module';
import { AdminModule } from './admin/admin.module';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import {AuthInterceptor} from './shared/services/auth.interceptor';
import { SidemenuComponent } from './shared/layout/sidemenu/sidemenu.component';
import { ToastrModule } from 'ngx-toastr';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxPaginationModule } from 'ngx-pagination';
import { ConfirmationDialogService } from './shared/services/confirmation-dialogue.service';
import { environment } from 'src/environments/environment';
import { Configuration } from 'msal';
import { MsalModule } from '@azure/msal-angular';
import { PublicClientApplication, InteractionType } from '@azure/msal-browser';
import { LoginComponent } from './user/login/login.component';
import { BehaviorSubject } from 'rxjs';
//import { MsalModule } from "@azure/msal-angular";

//node_modules\@azure\msal-angular\msal.service.d.ts



const isIE = window.navigator.userAgent.indexOf('MSIE ') > -1 || window.navigator.userAgent.indexOf('Trident/') > -1;






@NgModule({
  declarations: [
    AppComponent,
   
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    UserModule,
    FormsModule,
    ReactiveFormsModule,
    EmployeeModule,
    NgxPaginationModule,
    AdminModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    Ng2SearchPipeModule,
    FormsModule,
    NgbModule,
    HttpClientModule,
    //MsalModule
    MsalModule.forRoot( new PublicClientApplication({
      auth: {
        clientId: 'ef740363-2127-46d1-8d99-a6399a69f1a7', // This is your client ID
        authority: 'https://login.microsoftonline.com/13ec0e67-00c5-44c4-8bdb-52adb4a2feae', // This is your tenant ID
        redirectUri: 'http://localhost:4200'// This is your redirect URI
      },
      cache: {
        cacheLocation: 'sessionStorage',
        storeAuthStateInCookie: isIE, // Set to true for Internet Explorer 11
      }
    }), {
      interactionType: InteractionType.Popup, // MSAL Guard Configuration
      authRequest: {
        scopes: ['user.read'],
        state: "page_url"
      },
      loginFailedRoute: "/login" 
  }, null)

  ],    
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true},SidemenuComponent, ConfirmationDialogService],
  bootstrap: [AppComponent]
})

//provide: MSAL_CONFIG,
   // useFactory: MSALConfigFactory
 // },
export class AppModule { }

function MSALConfigFactory(): Configuration {
  return {
    auth: {
      clientId: environment.clientId,
      authority: environment.authority,
      validateAuthority: true,
      redirectUri: environment.redirectUrl,
      postLogoutRedirectUri: environment.redirectUrl,
      navigateToLoginRequestUrl: true
    },
    cache: {
      storeAuthStateInCookie: false,
    }
  };
}
