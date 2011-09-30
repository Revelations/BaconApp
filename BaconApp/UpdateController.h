//
//  UpdateController.h
//  BaconApp
//
//  Created by Donovan Hoffman on 1/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>


@interface UpdateController : UIViewController {
    IBOutlet UILabel *output;
}

-(IBAction)Update:(id)sender;
-(IBAction)CarryOn:(id)sender;

@end

