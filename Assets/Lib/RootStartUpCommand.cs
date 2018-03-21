using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.command.impl;

public class RootStartUpCommand : EventCommand {

    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject contextView { get; set; }

    public override void Execute()
    {
        //var template = new { success = 0, message = string.Empty, data = new ORC.OSS_Access_Info() };
        //var reader = new JsonFx.Json.JsonReader();
        //var output = reader.Read(ORC.OSSService.args, template);

        //if(output.success == 20)
        //Debug.Log(output.data.policy);

    }
}
