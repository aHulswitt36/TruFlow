export interface USGSData {
    value: USGSValue;
}

export interface USGSValue {
    timeSeries: TimeSeries[];
}

export interface TimeSeries {
    sourceInfo: SourceInfo;
    variable: Variable;
    values: Values[];
    name: string;
}

export interface SourceInfo {
    siteName: string;
}

export interface Variable {
    variableName: string;
    variableDescription: string;
    unit: Unit;
}

export interface Values {
    value: Value[];
    calculatedValue: string;
}

export interface Unit {
    unitCode: string;
}

export interface Value {
    value: string;
    dateTime: Date;
}
