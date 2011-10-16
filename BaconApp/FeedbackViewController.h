//
//  FeedbackController.h
//  BaconApp
//
//  Created by Donovan Hoffman on 12/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "ModalViewController.h"


@interface FeedbackViewController : ModalViewController <UITextViewDelegate, UITextFieldDelegate> {
	IBOutlet UITextField * textFieldNumber;
	IBOutlet UITextField * textFieldNationality;
	IBOutlet UITextField * textFieldSighted;
	
	IBOutlet UITextView  * textViewMiscellaneous;
	IBOutlet UITextField * textFieldMiscellaneous;
	
	IBOutlet UIScrollView * scrollView;
    
    UIButton *doneButton;
    BOOL displayAdditionalDoneButton;
}

-(IBAction)SendFeedback:(id)sender;
-(IBAction)Cancel:(id)sender;
@end
