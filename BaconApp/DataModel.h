//
//  DataModel.h
//  BaconApp
//
//  Created by Donoan Hoffman on 6/09/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "HistoryItem.h"

@interface DataModel : NSObject {
	NSMutableArray * history;
    HistoryItem * current; 
}

@property(nonatomic, retain) NSMutableArray * history;

@property(nonatomic, retain) HistoryItem * current;

@end
