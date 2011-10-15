//
//  FeedbackController.h
//  BaconApp
//
//  Created by Donovan Hoffman on 12/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>


@interface FeedbackController : UIViewController {
	IBOutlet UITextField *  numberTextField;
	IBOutlet UITextField *  nationalityTextField;
	IBOutlet UITextField *  seenTextField;
	IBOutlet UITextView  *  feedBackTextView;
}
-(IBAction)SendFeedback:(id)sender;
-(IBAction)Cancel:(id)sender;
@end
