//
//  SettingsController.h
//  BaconApp
//
//  Created by Russell Fredericks on 14/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>


@interface SettingsController : UIViewController {
	
	IBOutlet UITextField * Fontsize;
	IBOutlet UITextField * Serverip;
}
-(IBAction)acceptbutton_click;
@end