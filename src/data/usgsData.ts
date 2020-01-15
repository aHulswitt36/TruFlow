export interface USGSData{
    value: USGSValue
}

export interface USGSValue{
    timeSeries: Array<TimeSeries>
}

export interface TimeSeries{
    sourceInfo: SourceInfo,
    variable: Variable,
    values: Array<Values>,
    name: string
}

export interface SourceInfo{
    siteName: string    
}

export interface Variable{
    variableName: string,
    variableDescription: string,
    unit: Unit
}

export interface Values{
    value: Array<Value>,
    calculatedValue: string
}

export interface Unit{
    unitCode: string
}

export interface Value{
    value: string,
    dateTime: Date
}