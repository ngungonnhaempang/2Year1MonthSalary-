/// <reference types="node" />
import { ISizeCalculationResult } from './types/interface';
declare type CallbackFn = (e: Error | null, r?: ISizeCalculationResult) => void;
export default imageSize;
export declare function imageSize(input: Buffer | string): ISizeCalculationResult;
export declare function imageSize(input: string, callback: CallbackFn): void;
export declare const setConcurrency: (c: number) => void;
export declare const types: string[];
