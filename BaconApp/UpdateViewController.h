//
//  UpdateController.h
//  BaconApp
//
//  Created by Donovan Hoffman on 1/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "ModalViewController.h"


@interface UpdateViewController : ModalViewController {
	IBOutlet UILabel *output;
	IBOutlet UIProgressView *progBar;
}

@property(nonatomic,retain) IBOutlet UIProgressView *progBar;
-(IBAction)Update:(id)sender;
-(IBAction)CarryOn:(id)sender;

@end

