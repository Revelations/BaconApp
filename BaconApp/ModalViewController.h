//
//  ModalViewController.h
//  BaconApp
//
//  Created by Shii Kayano on 16/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "ModalViewControllerDelegate.h"

@interface ModalViewController : UIViewController {
	// A weak reference to the delegate
	id<ModalViewControllerDelegate> delegate;
}

@property (nonatomic, assign) id<ModalViewControllerDelegate> delegate;


@end
