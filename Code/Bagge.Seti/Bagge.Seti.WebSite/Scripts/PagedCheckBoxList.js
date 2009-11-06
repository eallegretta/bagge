(function($) {

	$.fn.pagedCheckBoxList = function(options) {
		var config = $.extend({}, $.fn.pagedCheckBoxList.defaults, options);


		function showPage(table, pageIndex, pageCount, rowCount) {
			$("tr.item", table).hide();
			$("tr.item[page=" + pageIndex + "]").show();
			table.data("currentPageIndex", pageIndex);
			createPager(table, pageCount, rowCount);
		}

		function createPagerSpan(index) {
			return $("<span class='current'>" + (index + 1) + "</span>");
		}
		function createPagerLink(table, index, pageCount, rowCount) {
			return $("<a href='javascript:void(0)'>" + (index + 1) + "</a>").click(function() {
				showPage(table, index, pageCount, rowCount);
			});
		}

		function getItemsCountText(currentPageIndex, pageCount, rowCount) {

			var startRecord = currentPageIndex * config.pageSize + 1;
			var endRecord = (startRecord + config.pageSize - 1 < rowCount) ? startRecord + config.pageSize - 1 : rowCount;

			return config.itemsCountText.replace("{0}", startRecord).replace("{1}", endRecord).replace("{2}", rowCount);
		}

		function createPager(table, pageCount, rowCount) {
			if (pageCount > 1) {
				var currentPageIndex = table.data("currentPageIndex");

				var row = $("tr.___pagerRow", table);
				if (row.length > 0)
					row.html("");
				else {
					row = $("<tr class='___pagerRow' />", table).addClass(config.pagerCssClass);
					table.append(row);
				}
				var td = $("<td colspan='2' />");
				row.append(td);
				var pagerTable = $("<table />");
				var pagerTableRow = $("<tr />");
				td.append(pagerTable);
				pagerTable.append(pagerTableRow);

				pagerTableRow.append($("<td class='firstCell' />").html(config.pageClauseText));

				for (var index = 0; index < pageCount; index++) {
					var pagerButton = $("<td />");
					pagerTableRow.append(pagerButton);
					if (index == currentPageIndex) {
						pagerButton.append(createPagerSpan(index));
					}
					else {
						pagerButton.append(createPagerLink(table, index, pageCount, rowCount));
					}
				}
				pagerTableRow.append($("<td class='lastCell' />").html(getItemsCountText(currentPageIndex, pageCount, rowCount)));

			}
		}

		this.each(function() {
			var table = $(this)
					.attr("cellpadding", config.cellpadding)
					.attr("cellspacing", config.cellspacing)
					.css("borderCollapse", "collapse")
					.width(config.width);
			var rowCount = $("tr", table).length;
			var pageCount = Math.ceil(rowCount / config.pageSize);
			table.data("currentPageIndex", 0);

			var pageIndex = 0;
			var counter = 1;
			$("tr", table).each(function() {
				if (counter > config.pageSize) {
					counter = 1;
					pageIndex++;
				}
				counter++;

				var label = $("label", this);
				$(this)
					.find("td")
						.end()
					.append($("<td />").append(label).width('100%'))
					.addClass("item")
					.attr("page", pageIndex);
			});
			table
                .find("tr:even")
                    .addClass(config.rowCssClass)
                    .end()
				.find("tr:odd")
					.addClass(config.alternateRowCssClass)
					.end();
			showPage(table, 0, pageCount, rowCount);
			table.prepend("<tr><th width='20'></th><th>" + config.title + "</th></tr>");
		});

		return this;

	};

	$.fn.pagedCheckBoxList.defaults = {
		title: '',
		width: 600,
		headerRowCssClass: '',
		rowCssClass: '',
		alternateRowCssClass: '',
		pagerCssClass: '',
		pageSize: 15,
		pageClauseText: '',
		itemsCountText: '',
		cellpadding: 0,
		cellspacing: 0
	}

})(jQuery);