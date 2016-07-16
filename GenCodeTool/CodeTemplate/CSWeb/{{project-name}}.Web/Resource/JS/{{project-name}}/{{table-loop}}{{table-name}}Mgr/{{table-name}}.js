{{if:section}}
Validate({
	rule:{{{if:loop:col|nopk|nofk|rlast:,}}
		{{col:name}} : {{{if:col:required}}
            required: true,{{/if:col:required}}{{if:col:max}}
            max: 111,{{/if:col:max}}{{if:col:min}}
            min: 111,{{/if:col:min}}{{if:col:unique}}
            CheckSame: 111,{{/if:col:unique}}{{rlast}},{{/rlast}}
		},
        {{/if:loop:col}}
	},
	messages:{{{if:loop:col|nopk|nofk|rlast:,}}
		col1 : {{{if:col:required}}
            required: true,{{/if:col:required}}{{if:col:max}}
            max: 222,{{/if:col:max}}{{if:col:min}}
            min: 222,{{/if:col:min}}{{if:col:unique}}
            CheckSame: 222,{{/if:col:unique}}{{rlast}},{{/rlast}}
		},
        {{/if:loop:col}}
	}
});
{{/if:section}}

{{loop:col|rfirst:,: }}
query.AppendLine(@"    ,@{{col:name}} ");{{/loop:col}}