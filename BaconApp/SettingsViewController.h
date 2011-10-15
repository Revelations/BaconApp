//
//  SettingsViewController.h
//  BaconApp
//
//  Created by Donoan Hoffman on 15/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "ModalViewController.h"


@interface SettingsViewController : ModalViewController {
	IBOutlet UITextField * txtFontSize;
	IBOutlet UITextField * txtIPAddress;
}

@property (nonatomic, retain) IBOutlet UITextField * txtFontSize;
@property (nonatomic, retain) IBOutlet UITextField * txtIPAddress;

-(IBAction)applyChangesPressed:(id)sender;
-(IBAction)discardChangesPressed:(id)sender;
-(IBAction)updateButtonPressed:(id)sender;


@end
