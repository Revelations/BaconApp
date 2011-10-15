//
//  AnswerSelectionController.h
//  BaconApp
//
//  Created by Donoan Hoffman on 16/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "GameViewController.h"

@interface AnswerSelectionController : UIViewController {

	IBOutlet  UITextView * optionA;
	IBOutlet  UITextView * optionB;
	IBOutlet  UITextView * optionC;
	IBOutlet  UITextView * optionD;
	NSNumber * question;
}


-(IBAction)btnOptionAPressed:(id)sender;
-(IBAction)btnOptionBPressed:(id)sender;
-(IBAction)btnOptionCPressed:(id)sender;
-(IBAction)btnOptionDPressed:(id)sender;

@property (nonatomic, retain) NSNumber * question;
@property (nonatomic, retain) GameViewController * parent;

@end
