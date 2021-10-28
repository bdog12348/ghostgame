// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Ghost"",
            ""id"": ""17a57225-e14a-420a-876d-7cc925346459"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""92795e1d-23db-4c78-aa35-4c4888b26548"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Possess"",
                    ""type"": ""Button"",
                    ""id"": ""2bf6f79b-41d5-4177-b8c8-cf240b3c4614"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""3843ad6c-a84a-47bc-8d58-786daf57cfa9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""3623b130-33d3-4cc6-b885-8dbc39b64be6"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8cdadf27-b32f-45e9-9121-83d7e1f0c956"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1d8b29f1-467a-4b97-bacc-4fedadf0ae34"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b314c923-f4d3-4db0-8d77-083925389b04"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""725bcb2b-2d02-4bd7-9344-f8a48050e02c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""2a53935c-6ded-49a0-a552-ea302237c4f9"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5b6a8293-a5ae-4d53-a48d-882283e0a3a1"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c35aac8d-3dfc-4ba9-b319-3e978af5677d"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f4cce054-fe9f-4851-b588-79ff66cc4639"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""07c0c6e9-6f3c-42d9-961f-00d35462b8db"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a8d7dff9-e240-4355-9a80-d999b257a6a9"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""73683b23-f982-4f72-a22b-0d3b9b8fa251"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e719f120-cf72-412a-b2c7-3c80d077f309"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""82e94fab-f6f9-4634-8a8d-4a053adec7a4"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a373a72c-e52e-4451-802b-1b3e79cb1de7"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Possess"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f2ee0e6a-3949-4f22-b0df-7b665598f3b2"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Possess"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Roomba"",
            ""id"": ""9c5029b7-e118-4a02-952a-5426435da7f4"",
            ""actions"": [
                {
                    ""name"": ""Possess"",
                    ""type"": ""Button"",
                    ""id"": ""cce07735-d8f0-4a3f-af49-a8a36db2d88f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d37e3a8b-b9c2-4622-8822-78e09ab7c854"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7158470c-2e9c-4ba0-97cb-dc124d131884"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Possess"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7d550bb-ec33-4a20-8e76-fcdad1f7e435"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Possess"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""edc9cff5-e142-4dce-beba-ce042c9ea187"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9eb443ea-b350-499c-ab3d-6001a6daeeee"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9a6f044f-46fc-4576-821c-163bdac47160"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Broom"",
            ""id"": ""1808c0ed-2db2-412d-9300-21547dc0c7a7"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7fd0f08a-4519-41f5-b0aa-bf29c92ca173"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Possess"",
                    ""type"": ""Button"",
                    ""id"": ""63c141db-b7b1-4b33-a3e8-e097e29a41de"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e983f0a1-e63a-4265-9e7a-0343fd66ac3f"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Possess"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""3f764812-50ae-4f80-8e09-2edf76fc196e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""858ed06c-ec69-4661-aec8-d56a6be2b91e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""cda7ca88-b93c-49d7-bf9f-63bdafaf3a54"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""fa22b5d3-a08f-4385-ba68-c713d3cbdf5c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""50846773-63cf-4921-9fc1-d256adbd859b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""e6120727-99a9-4d2f-a70f-c926be831532"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""854d4f52-7772-484d-8063-18eac94e92a5"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""765392fe-b265-4b64-8c8e-17ffe7cc34d9"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5c1032b1-288c-45b6-a27a-001b3421fdbf"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f419b780-59bb-4753-999b-c851a09d72c8"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""13d17c67-1ff0-4dec-acff-a7d2e1453a5b"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e95868da-4267-4111-b603-1729e8c03bf8"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""cf214649-1cc8-4e70-97cf-58541953a8df"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""91eeade0-b77a-4810-9c76-fa35c034cfb4"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""151bd756-4c0d-4a8a-a87d-b254b397c1f5"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Dustpan"",
            ""id"": ""768df180-903c-441b-b7d2-1c0a98a31fbb"",
            ""actions"": [
                {
                    ""name"": ""Possess"",
                    ""type"": ""Button"",
                    ""id"": ""a7be0aee-8a2e-4014-8170-0253c062f19f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""58cd69e5-46c7-4a2f-b988-0ef540b931a0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""735dcd74-2318-44dc-b008-f5180c65327a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""0d63b960-0d82-42a5-8668-955572e9f029"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""89077c5a-fb05-4024-9459-9e870e03e76e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b7dab7e7-88c5-47c7-be22-847f24e21dbb"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""760211d0-d10c-42fe-831a-b31eefad79c7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""603ae947-cd8c-4669-9e53-9d56d3322982"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ee5339bc-b6a7-4768-a022-c2b493fd0627"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""003484ba-402f-4b70-9cb3-9ff2f88c3553"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1e628fea-fe54-48eb-8170-9c1e9b36502d"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1160974e-9ee0-496a-8360-57e209bef3e2"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""ad633987-f259-4bad-b09d-85924309d127"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""dc55f931-1697-48db-bbcd-6b849759f3ef"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a8d4a661-c8e9-4522-878f-34bea986243b"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2acb64cd-d738-4a58-946f-47367b606cd8"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b6711e87-e48c-43a7-901e-6eb79b102ab6"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9947600c-35d3-408b-80b4-2efa91d9062e"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Possess"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8314b2cb-55f6-4f53-a391-cc1290484238"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Possess"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Ghost
        m_Ghost = asset.FindActionMap("Ghost", throwIfNotFound: true);
        m_Ghost_Move = m_Ghost.FindAction("Move", throwIfNotFound: true);
        m_Ghost_Possess = m_Ghost.FindAction("Possess", throwIfNotFound: true);
        // Roomba
        m_Roomba = asset.FindActionMap("Roomba", throwIfNotFound: true);
        m_Roomba_Possess = m_Roomba.FindAction("Possess", throwIfNotFound: true);
        m_Roomba_Move = m_Roomba.FindAction("Move", throwIfNotFound: true);
        // Broom
        m_Broom = asset.FindActionMap("Broom", throwIfNotFound: true);
        m_Broom_Move = m_Broom.FindAction("Move", throwIfNotFound: true);
        m_Broom_Possess = m_Broom.FindAction("Possess", throwIfNotFound: true);
        // Dustpan
        m_Dustpan = asset.FindActionMap("Dustpan", throwIfNotFound: true);
        m_Dustpan_Possess = m_Dustpan.FindAction("Possess", throwIfNotFound: true);
        m_Dustpan_Move = m_Dustpan.FindAction("Move", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Ghost
    private readonly InputActionMap m_Ghost;
    private IGhostActions m_GhostActionsCallbackInterface;
    private readonly InputAction m_Ghost_Move;
    private readonly InputAction m_Ghost_Possess;
    public struct GhostActions
    {
        private @PlayerControls m_Wrapper;
        public GhostActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Ghost_Move;
        public InputAction @Possess => m_Wrapper.m_Ghost_Possess;
        public InputActionMap Get() { return m_Wrapper.m_Ghost; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GhostActions set) { return set.Get(); }
        public void SetCallbacks(IGhostActions instance)
        {
            if (m_Wrapper.m_GhostActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GhostActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GhostActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GhostActionsCallbackInterface.OnMove;
                @Possess.started -= m_Wrapper.m_GhostActionsCallbackInterface.OnPossess;
                @Possess.performed -= m_Wrapper.m_GhostActionsCallbackInterface.OnPossess;
                @Possess.canceled -= m_Wrapper.m_GhostActionsCallbackInterface.OnPossess;
            }
            m_Wrapper.m_GhostActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Possess.started += instance.OnPossess;
                @Possess.performed += instance.OnPossess;
                @Possess.canceled += instance.OnPossess;
            }
        }
    }
    public GhostActions @Ghost => new GhostActions(this);

    // Roomba
    private readonly InputActionMap m_Roomba;
    private IRoombaActions m_RoombaActionsCallbackInterface;
    private readonly InputAction m_Roomba_Possess;
    private readonly InputAction m_Roomba_Move;
    public struct RoombaActions
    {
        private @PlayerControls m_Wrapper;
        public RoombaActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Possess => m_Wrapper.m_Roomba_Possess;
        public InputAction @Move => m_Wrapper.m_Roomba_Move;
        public InputActionMap Get() { return m_Wrapper.m_Roomba; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RoombaActions set) { return set.Get(); }
        public void SetCallbacks(IRoombaActions instance)
        {
            if (m_Wrapper.m_RoombaActionsCallbackInterface != null)
            {
                @Possess.started -= m_Wrapper.m_RoombaActionsCallbackInterface.OnPossess;
                @Possess.performed -= m_Wrapper.m_RoombaActionsCallbackInterface.OnPossess;
                @Possess.canceled -= m_Wrapper.m_RoombaActionsCallbackInterface.OnPossess;
                @Move.started -= m_Wrapper.m_RoombaActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_RoombaActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_RoombaActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_RoombaActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Possess.started += instance.OnPossess;
                @Possess.performed += instance.OnPossess;
                @Possess.canceled += instance.OnPossess;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public RoombaActions @Roomba => new RoombaActions(this);

    // Broom
    private readonly InputActionMap m_Broom;
    private IBroomActions m_BroomActionsCallbackInterface;
    private readonly InputAction m_Broom_Move;
    private readonly InputAction m_Broom_Possess;
    public struct BroomActions
    {
        private @PlayerControls m_Wrapper;
        public BroomActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Broom_Move;
        public InputAction @Possess => m_Wrapper.m_Broom_Possess;
        public InputActionMap Get() { return m_Wrapper.m_Broom; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BroomActions set) { return set.Get(); }
        public void SetCallbacks(IBroomActions instance)
        {
            if (m_Wrapper.m_BroomActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_BroomActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_BroomActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_BroomActionsCallbackInterface.OnMove;
                @Possess.started -= m_Wrapper.m_BroomActionsCallbackInterface.OnPossess;
                @Possess.performed -= m_Wrapper.m_BroomActionsCallbackInterface.OnPossess;
                @Possess.canceled -= m_Wrapper.m_BroomActionsCallbackInterface.OnPossess;
            }
            m_Wrapper.m_BroomActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Possess.started += instance.OnPossess;
                @Possess.performed += instance.OnPossess;
                @Possess.canceled += instance.OnPossess;
            }
        }
    }
    public BroomActions @Broom => new BroomActions(this);

    // Dustpan
    private readonly InputActionMap m_Dustpan;
    private IDustpanActions m_DustpanActionsCallbackInterface;
    private readonly InputAction m_Dustpan_Possess;
    private readonly InputAction m_Dustpan_Move;
    public struct DustpanActions
    {
        private @PlayerControls m_Wrapper;
        public DustpanActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Possess => m_Wrapper.m_Dustpan_Possess;
        public InputAction @Move => m_Wrapper.m_Dustpan_Move;
        public InputActionMap Get() { return m_Wrapper.m_Dustpan; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DustpanActions set) { return set.Get(); }
        public void SetCallbacks(IDustpanActions instance)
        {
            if (m_Wrapper.m_DustpanActionsCallbackInterface != null)
            {
                @Possess.started -= m_Wrapper.m_DustpanActionsCallbackInterface.OnPossess;
                @Possess.performed -= m_Wrapper.m_DustpanActionsCallbackInterface.OnPossess;
                @Possess.canceled -= m_Wrapper.m_DustpanActionsCallbackInterface.OnPossess;
                @Move.started -= m_Wrapper.m_DustpanActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_DustpanActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_DustpanActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_DustpanActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Possess.started += instance.OnPossess;
                @Possess.performed += instance.OnPossess;
                @Possess.canceled += instance.OnPossess;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public DustpanActions @Dustpan => new DustpanActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IGhostActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnPossess(InputAction.CallbackContext context);
    }
    public interface IRoombaActions
    {
        void OnPossess(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
    }
    public interface IBroomActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnPossess(InputAction.CallbackContext context);
    }
    public interface IDustpanActions
    {
        void OnPossess(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
    }
}
