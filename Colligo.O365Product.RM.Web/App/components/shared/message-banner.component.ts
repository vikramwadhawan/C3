import {
    Component,
    OnInit,
    AfterViewInit,
    Input,
    Output,
    EventEmitter
} from '@angular/core';

@Component({
    selector: 'message-banner',
    templateUrl: './message-banner.component.html'
})

export class MessageBannerComponent implements OnInit, AfterViewInit {
    @Input('type') _type:string;
    @Input('message') _message:string;
    @Output() onMessageBannerClose = new EventEmitter(); 
    
    constructor() { }

    ngOnInit() { }

    ngAfterViewInit(){

    }

    /**
     * on close icon click
     */
    private onClose() {
        //emit event to parent
        this.onMessageBannerClose.emit();
    }
}