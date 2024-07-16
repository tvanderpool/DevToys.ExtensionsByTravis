namespace DevToys.Api;

public static class Extensions {

    //
    // Summary:
    //     Sets the DevToys.Api.IUISwitch.OnText & DevToys.Api.IUISwitch.OffText of the switch.
    public static IUISwitch Text(this IUISwitch element, string? text)
        => element.OnText(text).OffText(text);

    //
    // Summary:
    //     Sets the DevToys.Api.IUISwitch.OnText & DevToys.Api.IUISwitch.OffText of the switch.
    public static IUISwitch Text(this IUISwitch element, string? onText, string? offText)
        => element.OnText(onText).OffText(offText);

}
