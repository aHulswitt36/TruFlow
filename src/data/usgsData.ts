export interface USGSData{
    value: USGSValue
}

export interface USGSValue{
    timeSeries: Array<TimeSeries>
}

export interface TimeSeries{
    sourceInfo: SourceInfo,
    variable: Variable,
    values: Values,
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
    values: Array<Value>
}

export interface Unit{
    unitCode: string
}

export interface Value{
    value: string,
    dateTime: Date
}