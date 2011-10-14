//
//  Quiz.h
//  BaconApp
//
//  Created by Donovan Hoffman on 14/10/11.
//  Copyright 2011 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>


@interface Quiz : NSObject {
    NSMutableArray  * questions;
    NSString        * questionName;
}

#pragma mark - Properties
@property (retain, nonatomic) NSMutableArray * questions;
@property (retain, nonatomic) NSString * questionName;


#pragma mark - Methods
-(void)readQuizFile: (NSString *) filePath;
@end
