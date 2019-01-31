import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import { CONFIGURATION, MessageType } from './../../app.constant';
import { EventSettingUser } from './../../model/eventSettingUserSetup/EventSettingUser';
import { AgentTimeZoneModel, TimeZone } from './../../model/eventSettingUserSetup/agentTimeZoneModel';
import { Parser } from './../../helper/parser';
import { LocalStorageUtil } from './../../helper/localStorageUtil';
import { Component, OnInit, OnDestroy, AfterViewInit, ElementRef, ViewChild, Renderer, Input } from '@angular/core';
import { WindowRef } from '../../services/windowRef.service';
import { forEach } from '@angular/router/src/utils/collection';
import { AppUtil } from '../../helper/appUtil';
import { EventSettingUserService } from '../../services/eventsettinguser.service'
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';

declare var $: any;

@Component({
    selector: 'organization',
    templateUrl: './organization.component.html'
})

export class OrganizationComponent implements OnInit {

    constructor(private _wind: WindowRef,
        private _router: Router,
        private element: ElementRef,
        private _renderer: Renderer,
        private _eventSettingService: EventSettingUserService,
        private formBuilder: FormBuilder
    ) { 
    }   
    showAdgSection: boolean = false;
    registerForm: FormGroup;
    submitted = false;
    siteTenantUrl: string = Parser.getTenantUrl();
    public isProcessing: boolean = false;
    public showMessageBanner: boolean = false;
    public messageBannerType: string = '';
    public bannerMessage: string = '';
    public showEventUser: boolean = true;
    public availableTimeZones: Array<string> = [];
    public timeZones: Array<TimeZone> = [];
    public availableTimeHours: Array<string> = [];
    public availableTimeMinutes: Array<string> = [];
    public selectedTimeZone: string 
    public selectedTimeHour: string 
    public selectedTimeMinute: string 
    public formControlClass: any = { 'class': 'form-control' };

      
    ngOnInit() {
        this.loadUserForm();
        this.loadTimeZoneForm()
      
        }
     private loadUserForm() {

        try {
            this.registerForm = this.formBuilder.group({
                siteUrl: new FormControl({ value: this.siteTenantUrl, disabled: true }, Validators.required),
                ddlTimeZone: ['', [Validators.required]],
                ddlTimeHours: ['', [Validators.required]],
                ddlTimeMinutes: ['', [Validators.required]],
                spUserId: ['', [Validators.required, Validators.email]],
                spPassword: ['', Validators.required],
                spConfirmPassword: ['', Validators.required],
                isSameCredential: [false],
                adgUserId: ['', [Validators.required, Validators.email]],
                adgPassword: ['', [Validators.required]],
                adgConfirmPassword: ['', Validators.required],
            });

            this.loadEventSettingUser()
        }
        catch (e) {
            console.log(e);
            this.isProcessing = false;
            this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
        }

    }

    private loadTimeZoneForm()
    {
        this.setTimeZoneddl();
        this.setTime();
    }

    private setTimeZoneddl()
    {
         this.getTimeZone()

    }


    private  getTimeZone() {

        this._eventSettingService.getTimeZone()
            .subscribe((res) => {
                if (res != null && res != '') {
                  this.timeZones = <TimeZone[]>JSON.parse(JSON.stringify(res));
                 }
                
            });

    }

   private setTime() {

        if (this.availableTimeHours.length == 0) {

            for (var i = 0; i <= 23; i++) {
                var hour = i.toString().length == 1 ? "0" + i : i;
                this.availableTimeHours.push(hour.toString());
            }
         }
        if (this.availableTimeMinutes.length == 0) {

            for (var i = 0; i <= 59; i++) {
                var minute = i.toString().length == 1 ? "0" + i : i;
                this.availableTimeMinutes.push(minute.toString());
            }

        }


    }

    // convenience getter for easy access to form fields
    get f() { return this.registerForm.controls; }


    

    private saveUser() {

        try
        {
             // Saves or Updates the user data
            let objOrganizationUser = new EventSettingUser();
            objOrganizationUser.spUserId = this.registerForm.value.spUserId;
            objOrganizationUser.spUserPassword = this.registerForm.value.spPassword;
            objOrganizationUser.adgUserPassword = this.registerForm.value.adgPassword;
            objOrganizationUser.adgUserId = this.registerForm.value.adgUserId;
            objOrganizationUser.siteUrl = this.siteTenantUrl;
            let timeZoneDetails = new AgentTimeZoneModel();
            timeZoneDetails.timeZone = this.registerForm.value.ddlTimeZone;
            timeZoneDetails.time = this.registerForm.value.ddlTimeHours + ":" + this.registerForm.value.ddlTimeMinutes;
            timeZoneDetails.timeZoneDisplayName = this.registerForm.value.ddlTimeZone;
            objOrganizationUser.agentTimeZoneModel = timeZoneDetails;

            this.timeZones.filter((value) => {

                if (value.id == this.registerForm.value.ddlTimeZone) {
                    timeZoneDetails.timeZoneDisplayName = value.displayName;
         
                }

            })
            this._eventSettingService.saveEventSettingUser(objOrganizationUser)
                .subscribe((res) => {
                     if (res != 'fail' && res != '') {
                        this.isProcessing = false;
                        this.userFriendlyMessages(false, CONFIGURATION.constants.messages.SAVED_SUCCESSFULLY);
                        this.loadEventSettingUser();
                    }
                    else {
                        this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
                    }

                },
                    (err) => {
                        this.isProcessing = false;
                        this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
                    }
                )
        }
        catch (e) {
            console.log(e);
            this.isProcessing = false;
            this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
        }


    }


    private loadEventSettingUser()
    {
        // Loading initial data for the user
         let url: string = this.siteTenantUrl;

        this._eventSettingService.getEventSettingUser(url)
            .subscribe((res) => {
                if (res != 'fail' && res != '') {
                    this.getUserDatacallback(<EventSettingUser>JSON.parse(JSON.stringify(res)));
                }
            },
              (err) => {
                console.error(err);
                this.isProcessing = false;
                this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
            }
            )



    }


    private getUserDatacallback(user: EventSettingUser)
    {
        this.registerForm.controls['spUserId'].setValue(user.spUserId);
        this.registerForm.controls['spPassword'].setValue(user.spUserPassword);
        this.registerForm.controls['spConfirmPassword'].setValue(user.spUserPassword);
        this.registerForm.controls['adgUserId'].setValue(user.adgUserId);
        this.registerForm.controls['adgPassword'].setValue(user.adgUserPassword);
        this.registerForm.controls['adgConfirmPassword'].setValue(user.adgUserPassword);
        this.registerForm.controls['isSameCredential'].setValue(false);
        if (user.agentTimeZoneModel) {
            this.registerForm.controls['ddlTimeZone'].setValue(user.agentTimeZoneModel.timeZone);
            var time = user.agentTimeZoneModel.time.split(":");
            if (time) {
                this.registerForm.controls['ddlTimeHours'].setValue(time[0]);
                this.registerForm.controls['ddlTimeMinutes'].setValue(time[1]);
            }

        }
        else {
            this.registerForm.controls['ddlTimeZone'].setValue('');
            this.registerForm.controls['ddlTimeHours'].setValue('');
            this.registerForm.controls['ddlTimeMinutes'].setValue('');
        }
        
        
        this.registerForm.updateValueAndValidity();
        this.isProcessing = false;
      
    }

    onSubmit() {
       
        this.submitted = true;
       // stop here if form is invalid
        if (this.registerForm.invalid) {
             return;
        }
        this.isProcessing = true;
        this.saveUser();
        
      }

    public goToHome() {
        this._router.navigate(['/dashboard']);
    }
    public resetPage() {
        this.siteTenantUrl = Parser.getTenantUrl();
        this.loadEventSettingUser();
        this.showAdgSection = false;

    }
    onNotificationClose() {
        this.showMessageBanner = false;
    }
    private userFriendlyMessages(isError: boolean, message: string) {
        this.messageBannerType = (isError) ? MessageType[MessageType.Error] : MessageType[MessageType.Notify];
        this.bannerMessage = message;
        this.showMessageBanner = true;
        setTimeout(() => {
            this.showMessageBanner = false;
        }, CONFIGURATION.constants.MSG_TIMEOUT)
    }
    public checkSameCredential() {

        this.showAdgSection = !this.showAdgSection
        if (this.showAdgSection) {
            this.registerForm.controls['adgUserId'].setValue(this.registerForm.value.spUserId);
            this.registerForm.controls['adgPassword'].setValue(this.registerForm.value.spPassword);
            this.registerForm.controls['adgConfirmPassword'].setValue(this.registerForm.value.spConfirmPassword);
            this.registerForm.updateValueAndValidity();
        }
        else {

            this.registerForm.controls['adgUserId'].setValue('');
            this.registerForm.controls['adgPassword'].setValue('');
            this.registerForm.controls['adgConfirmPassword'].setValue('');
            this.registerForm.updateValueAndValidity();
        }
    }
}

