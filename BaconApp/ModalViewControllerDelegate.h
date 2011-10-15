//
//  ModalViewControllerDelegate.h
//  BaconApp
//
//  Created by Shii Kayano on 16/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//
//  http://useyourloaf.com/blog/2010/5/3/ipad-modal-view-controllers.html

#import <UIKit/UIKit.h>

@protocol ModalViewControllerDelegate <NSObject>

-(void)didDismissModalView;

@end
