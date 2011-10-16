//
//  FeedbackController.h
//  BaconApp
//
//  Created by Donovan Hoffman on 12/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "ModalViewController.h"


@interface FeedbackViewController : ModalViewController {
	IBOutlet UITextField *  numberTextField;
	IBOutlet UITextField *  nationalityTextField;
	IBOutlet UITextField *  seenTextField;
	IBOutlet UITextView  *  feedBackTextView;

	
	IBOutlet UIScrollView *scrollview;
    IBOutlet UITextField *textField1;
    IBOutlet UITextField *textField2;
    IBOutlet UITextField *textField3;
    IBOutlet UITextField *textField4;
	
	BOOL displayKeyboard;
	CGPoint  offset;
	UITextField *Field;
}

@property(nonatomic,retain) IBOutlet UIScrollView *scrollview;
@property(nonatomic,retain) IBOutlet UITextField *textField1;
@property(nonatomic,retain) IBOutlet UITextField *textField2;
@property(nonatomic,retain) IBOutlet UITextField *textField3;
@property(nonatomic,retain) IBOutlet UITextField *textField4;


-(IBAction)SendFeedback:(id)sender;
-(IBAction)Cancel:(id)sender;
@end
