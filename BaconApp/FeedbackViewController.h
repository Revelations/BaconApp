//
//  FeedbackController.h
//  BaconApp
//
//  Copyright 2011 Team Bacon. All rights reserved.
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
    
    BOOL displayAdditionalDoneButton;
}

-(IBAction)SendFeedback:(id)sender;

@end
