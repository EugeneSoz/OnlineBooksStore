import { BrowserModule } from '@angular/platform-browser';
import { NgModule, LOCALE_ID } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

import localeRu from "@angular/common/locales/ru";
import { registerLocaleData } from '@angular/common';
import { DeleteMessageComponent } from './admin/shared/delete-message/delete-message.component';
import { AuthenticationModule } from './authentication/authentication.module';

registerLocaleData(localeRu, "ru");

@NgModule({
    declarations: [
        AppComponent,
        DeleteMessageComponent
    ],
    imports: [
        BrowserModule,
        RouterModule,
        HttpClientModule,
        AppRoutingModule,
        BrowserAnimationsModule,
        AuthenticationModule
    ],
    providers: [
        { provide: LOCALE_ID, useValue: 'ru' }
    ],
    entryComponents: [DeleteMessageComponent],
    bootstrap: [AppComponent]
})
export class AppModule { }
