//
//  SettingsViewController.h
//  BaconApp
//
//  Created by Donoan Hoffman on 15/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>


@interface SettingsViewController : UIViewController {
	IBOutlet UITextField * txtFontSize;
	IBOutlet UITextField * txtIPAddress;
}

@property (nonatomic, retain) IBOutlet UITextField * txtFontSize;
@property (nonatomic, retain) IBOutlet UITextField * txtIPAddress;

-(IBAction)applyChangesPressed:(id)sender;
-(IBAction)discardChangesPressed:(id)sender;

@end
