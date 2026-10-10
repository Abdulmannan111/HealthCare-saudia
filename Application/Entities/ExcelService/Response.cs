using System;
using System.Collections.Generic;

public class Response
{
    public Data? data { get; set; }
    public int? version { get; set; }
}

public class Data
{
    public string? _id { get; set; }
    public string? name { get; set; }
    public string? icon { get; set; }
    public string? projectId { get; set; }
    public MetaData? meta_data { get; set; }
    public string? slug { get; set; }
    public DateTime? updated_at { get; set; }
    public DateTime? created_at { get; set; }
    public bool? status { get; set; }
    public Schema? schema { get; set; }
    public string? saved_icon { get; set; }
    public bool? is_inspection_engine { get; set; }
    public string? campaign_title { get; set; }
    public bool? is_campaign { get; set; }
    public DateTime? start_date { get; set; }
    public object? deleted_at { get; set; }
}

public class MetaData
{
    public List<string>? roles { get; set; }
    public string? menuName { get; set; }
    public List<Region>? regions { get; set; }
    public List<string>? facility_types { get; set; }
}

public class Region
{
    public string label { get; set; }
    public string value { get; set; }
}

public class Schema
{
    public Form form { get; set; }
    public SchemaProperties schema { get; set; }
}

public class Form
{
    public int labelCol { get; set; }
    public int wrapperCol { get; set; }
    public string layout { get; set; }
    public string global_utils { get; set; }
    public string feedbackLayout { get; set; }
    public string size { get; set; }
}

public class SchemaProperties
{
    public Dictionary<string, Property> properties { get; set; }
    public object type { get; set; }
    public string x_designable_id { get; set; }
}

public class Property
{
    public string type { get; set; }
    public string title { get; set; }
    public string x_decorator { get; set; }
    public string x_component { get; set; }
    public object x_component_props { get; set; }
    public List<object> x_validator { get; set; }
    public object x_decorator_props { get; set; }
    public string name { get; set; }
    public bool required { get; set; }
    public string x_designable_id { get; set; }
    public int x_index { get; set; }
    public string x_reactions { get; set; }
    public string x_display { get; set; }
    public string x_pattern { get; set; }
    public string x_component_type { get; set; }
    public string x_component_props_type { get; set; }
    public string x_decorator_type { get; set; }
    public string x_decorator_props_type { get; set; }
    public string x_component_props_style { get; set; }
    public string x_component_props_content { get; set; }
    public string x_component_props_action { get; set; }
    public string x_component_props_placeholder { get; set; }
    public string x_component_props_format { get; set; }
    public string x_component_props_size { get; set; }
    public string x_component_props_label { get; set; }
    public string x_component_props_title { get; set; }
    public string x_component_props_textContent { get; set; }
    public string x_component_props_listType { get; set; }
    public string x_component_props_showTime { get; set; }
    public string x_component_props_showNow { get; set; }
    public string x_component_props_bordered { get; set; }
    public string x_component_props_allowClear { get; set; }
    public string x_component_props_showSearch { get; set; }
    public string x_component_props_autoFocus { get; set; }
    public string x_component_props_autoClearSearchValue { get; set; }
    public string x_component_props_filterOption { get; set; }
    public string x_component_props_defaultOpen { get; set; }
    public string x_component_props_defaultActiveFirstOption { get; set; }
    public string x_component_props_direction { get; set; }
    public string x_component_props_isClearSignature { get; set; }
    public string x_component_props_clearText { get; set; }
    public string x_component_props_isDanger { get; set; }
    public string x_component_props_height { get; set; }
    public string x_component_props_size_small { get; set; }
    public string x_component_props_type_default { get; set; }
    public string x_component_props_is_dynamic { get; set; }
    public string x_component_props_direction_ltr { get; set; }
    public string x_component_props_style_color { get; set; }
    public string x_component_props_style_fontWeight { get; set; }
    public string x_component_props_style_backgroundColor { get; set; }
    public string x_component_props_style_padding { get; set; }
    public string x_component_props_style_borderRadius { get; set; }
    public string x_component_props_style_cursor { get; set; }
    public string x_component_props_style_display { get; set; }
    public string x_component_props_style_height_auto { get; set; }
    public string x_component_props_style_flexDirection { get; set; }
    public string x_component_props_style_flexWrap { get; set; }
    public string x_component_props_style_alignItems { get; set; }
    public string x_component_props_style_justifyContent { get; set; }
    public string x_component_props_style_alignContent { get; set; }
    public string x_component_props_style_flexStart { get; set; }
    public string x_component_props_style_flexEnd { get; set; }
    public string x_component_props_style_flexCenter { get; set; }
    public string x_component_props_style_flexColumn { get; set; }
    public string x_component_props_style_flexRow { get; set; }
    public string x_component_props_style_flexRowReverse { get; set; }
    public string x_component_props_style_flexColumnReverse { get; set; }
    public string x_component_props_style_flexRowWrap { get; set; }
    public string x_component_props_style_flexColumnWrap { get; set; }
    public string x_component_props_style_flexRowWrapReverse { get; set; }
    public string x_component_props_style_flexColumnWrapReverse { get; set; }
    public string x_component_props_style_flexRowStart { get; set; }
    public string x_component_props_style_flexColumnStart { get; set; }
    public string x_component_props_style_flexRowEnd { get; set; }
    public string x_component_props_style_flexColumnEnd { get; set; }
    public string x_component_props_style_flexRowCenter { get; set; }
    public string x_component_props_style_flexColumnCenter { get; set; }
    public string x_component_props_style_flexRowSpaceBetween { get; set; }
    public string x_component_props_style_flexColumnSpaceBetween { get; set; }
    public string x_component_props_style_flexRowSpaceAround { get; set; }
    public string x_component_props_style_flexColumnSpaceAround { get; set; }
    public string x_component_props_style_flexRowSpaceEvenly { get; set; }
    public string x_component_props_style_flexColumnSpaceEvenly { get; set; }
    public string x_component_props_style_flexRowJustifyContent { get; set; }
    public string x_component_props_style_flexColumnJustifyContent { get; set; }
    public string x_component_props_style_flexRowAlignItems { get; set; }
    public string x_component_props_style_flexColumnAlignItems { get; set; }
    public string x_component_props_style_flexRowAlignContent { get; set; }
    public string x_component_props_style_flexColumnAlignContent { get; set; }
    public string x_component_props_style_flexRowAlignSelf { get; set; }
    public string x_component_props_style_flexColumnAlignSelf { get; set; }
    public string x_component_props_style_flexRowJustifySelf { get; set; }
    public string x_component_props_style_flexColumnJustifySelf { get; set; }
    public string x_component_props_style_flexRowJustifyItems { get; set; }
    public string x_component_props_style_flexColumnJustifyItems { get; set; }
    public string x_component_props_style_flexRowJustifyContentSpaceBetween { get; set; }
    public string x_component_props_style_flexColumnJustifyContentSpaceBetween { get; set; }
    public string x_component_props_style_flexRowJustifyContentSpaceAround { get; set; }
    public string x_component_props_style_flexColumnJustifyContentSpaceAround { get; set; }
    public string x_component_props_style_flexRowJustifyContentSpaceEvenly { get; set; }
    public string x_component_props_style_flexColumnJustifyContentSpaceEvenly { get; set; }
    public string x_component_props_style_flexRowJustifyContentStart { get; set; }
    public string x_component_props_style_flexColumnJustifyContentStart { get; set; }
    public string x_component_props_style_flexRowJustifyContentEnd { get; set; }
    public string x_component_props_style_flexColumnJustifyContentEnd { get; set; }
    public string x_component_props_style_flexRowJustifyContentCenter { get; set; }
    public string x_component_props_style_flexColumnJustifyContentCenter { get; set; }
   
}